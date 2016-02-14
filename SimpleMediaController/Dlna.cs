using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPNPLib;
using System.Threading;
using System.Xml.Linq;

namespace SimpleMediaController
{
    delegate void DevieScanCompleteAction();

    abstract class DLNADevices<T>: IDisposable
        where T: DLNADevice
    {
        protected abstract string URN { get; }

        public IEnumerable<T> Items
        {
            get
            {
                return _items.AsEnumerable<T>();
            }
        }
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
        public T this[string UID]
        {
            get
            {
                return _items.SingleOrDefault((i) =>
                {
                    return i.UID == UID;
                });
            }
        }

        public bool IsDeviceScanThreadRunning
        {
            get
            {
                return _device_scan_thread == null ? false : _device_scan_thread.ThreadState == ThreadState.Running;
            }
        }

        private List<T> _items;
        private Thread _device_scan_thread;

        public DLNADevices()
        {
            _items = new List<T>();
        }

        public void Dispose() 
        {
            if(IsDeviceScanThreadRunning)
            {
                _device_scan_thread.Abort();
            }
        }

        public bool CreateDeviceScanThread(DevieScanCompleteAction Action)
        {
            if (IsDeviceScanThreadRunning)
            {
                return false;
            }

            _device_scan_thread = new Thread(new ThreadStart(() =>
                {
                    _items.Clear();

                    UPnPDeviceFinder finder = new UPnPDeviceFinder();
                    UPnPDevices devices = finder.FindByType(URN, 0);

                    if (devices != null)
                    {
                        devices.OfType<UPnPDevice>().ToList()
                            .ForEach((d) =>
                            {
                                _items.Add((T)Activator.CreateInstance(typeof(T), new object[] { d }));
                            });
                    }

                    Action();
                }));
            return true;
        }

        public void StartDevceScanThread()
        {
            if (_device_scan_thread == null)
            {
                throw new Exception("スレッドが作成されていません");
            }

            _device_scan_thread.Start();
        }
    }

    abstract class DLNADevice
    {
        public string UID
        {
            get
            {
                return _device.UniqueDeviceName;
            }
        }
        public string FriendlyName
        {
            get
            {
                return _device.FriendlyName;
            }
        }

        protected UPnPDevice Device
        {
            get
            {
                return _device;
            }
        }

        protected List<DLNAService> Services
        {
            get
            {
                return _services;
            }
        }

        UPnPDevice _device;
        List<DLNAService> _services;

        public DLNADevice(UPnPDevice device)
        {
            _device = device;

            _services = new List<DLNAService>();
            device.Services.OfType<UPnPService>()
                .ToList().ForEach((s) =>
                {
                    _services.Add(CreateDLNAServiceInstance(s));
                });
        }

        protected abstract DLNAService CreateDLNAServiceInstance(UPnPService service);
    }

    abstract class DLNAService
    {
        protected UPnPService Service
        {
            get
            {
                return _service;
            }
        }

        UPnPService _service;

        public DLNAService(UPnPService service)
        {
            _service = service;
        }
    }

    class DLNAMediaServers : DLNADevices<DLNAMediaServer>
    {
        protected override string URN
        {
            get { return "urn:schemas-upnp-org:device:MediaServer:1"; }
        }
    }
    class DLNAMediaServer : DLNADevice
    {
        public class ContentDirectoryService : DLNAService
        {
            public ContentDirectoryService(UPnPService service)
                : base(service)
            {
            }

            public XElement BrowseDirectChildren(string objectId, string filter, uint startIndex, uint requestedCount, string sortCriteria)
            {
                object result = new object[4];
                Service.InvokeAction("Browse", new object[] {
                    objectId,
                    "BrowseDirectChildren",
                    filter,
                    startIndex,
                    requestedCount,
                    sortCriteria
                }, ref result);

                return XElement.Parse((string)((object[])result)[0]);
            }

            public XElement BrowseMetaData(string objectId, string filter, uint startIndex, uint requestedCount, string sortCriteria)
            {
                object result = new object[4];
                Service.InvokeAction("Browse", new object[] {
                    objectId,
                    "BrowseMetaData",
                    filter,
                    startIndex,
                    requestedCount,
                    sortCriteria
                }, ref result);

                return XElement.Parse((string)((object[])result)[0]);
            }
        }

        public bool HasContentDirectory
        {
            get
            {
                return ContentDirectory != null;
            }
        }
        public ContentDirectoryService ContentDirectory
        {
            get
            {
                return Services.OfType<ContentDirectoryService>().SingleOrDefault();
            }
        }

        public DLNAMediaServer(UPnPDevice device)
            : base(device)
        {
        }

        protected override DLNAService CreateDLNAServiceInstance(UPnPService service)
        {
            DLNAService result = null;
            switch (service.Id)
            {
                case "urn:upnp-org:serviceId:ContentDirectory":
                    result = new ContentDirectoryService(service);
                    break;
            }

            return result;
        }
    }



    class DLNAMediaRenders : DLNADevices<DLNAMediaRender>
    {
        protected override string URN
        {
            get { return "urn:schemas-upnp-org:device:MediaRenderer:1"; }
        }
    }
    class DLNAMediaRender : DLNADevice
    {
        public class AVTransportService : DLNAService
        {
            public struct PositionInfo
            {
                public uint Track;
                public string TrackDuration;
                public string TrackMetaData;
                public string TrackURI;
                public string RelTime;
                public string AbsTime;
                public int RelCount;
                public int AbsCount;
            }

            public struct TransportInfo
            {
                public string CurrentTransportState;
                public string CurrentTransportStatus;
                public string CurrentSpeed;
            }

            public AVTransportService(UPnPService service)
                : base(service)
            {
            }

            public PositionInfo GetPositionInfo()
            {
                object result = new object[8];

                Service.InvokeAction("GetPositionInfo", new object[] { 0 }, ref result);

                object[] results = (object[])result;
                return new PositionInfo
                {
                    Track = (uint)results[0],
                    TrackDuration = (string)results[1],
                    TrackMetaData = (string)results[2],
                    TrackURI = (string)results[3],
                    RelTime = (string)results[4],
                    AbsTime = (string)results[5],
                    RelCount = (int)results[6],
                    AbsCount = (int)results[7]
                };
            }

            public TransportInfo GetTransportInfo()
            {
                object result = new object[3];

                Service.InvokeAction("GetTransportInfo", new object[] { 0 }, ref result);

                object[] results = (object[])result;
                return new TransportInfo
                {
                    CurrentTransportState = (string)results[0],
                    CurrentTransportStatus= (string)results[1],
                    CurrentSpeed = (string)results[2]
                };
            }

            public void Play()
            {
                object tmp = new object();
                Service.InvokeAction("Play", new object[] { 0, "1" }, ref tmp);
            }

            public void Pause()
            {
                object tmp = new object();
                Service.InvokeAction("Pause", new object[] { 0 }, ref tmp);
            }

            public void SetAVTransportURI(string currentURI, string currentURIMetaData)
            {
                object tmp = new object();
                Service.InvokeAction("SetAVTransportURI", new object[] { 0, currentURI, currentURIMetaData }, ref tmp);
            }

            public void Stop()
            {
                object tmp = new object();
                Service.InvokeAction("Stop", new object[] { 0 }, ref tmp);
            }
        }

        public class RenderingControlService : DLNAService
        {
            public XElement LastChange
            {
                get
                {
                    return XElement.Parse(Service.QueryStateVariable("LastChange"));
                }
            }

            public RenderingControlService(UPnPService service)
                : base(service)
            {
            }
        }

        public bool HasAVTransport
        {
            get
            {
                return AVTransport != null;
            }
        }
        public AVTransportService AVTransport
        {
            get
            {
                return Services.OfType<AVTransportService>().SingleOrDefault();
            }
        }


        public bool HasRenderingControl
        {
            get
            {
                return RenderingControl != null;
            }
        }
        public RenderingControlService RenderingControl
        {
            get
            {
                return Services.OfType<RenderingControlService>().SingleOrDefault();
            }
        }
        
        public DLNAMediaRender(UPnPDevice device)
            : base(device)
        {
        }

        protected override DLNAService CreateDLNAServiceInstance(UPnPService service)
        {
            DLNAService result = null;
            switch (service.Id)
            {
                case "urn:upnp-org:serviceId:AVTransport":
                    result = new AVTransportService(service);
                    break;

                case"urn:upnp-org:serviceId:RenderingControl":
                    result = new RenderingControlService(service);
                    break;
            }

            return result;
        }
    }

    class DLNAMetaData
    {
        public static XNamespace NS = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/";
        public static XNamespace NS_DC = "http://purl.org/dc/elements/1.1/";
        public static XNamespace NS_UPNP = "urn:schemas-upnp-org:metadata-1-0/upnp/";
    }

    class DLNAEventMetaData
    {
        public static XNamespace NS = "urn:schemas-upnp-org:metadata-1-0/AVT/";
    }
}
