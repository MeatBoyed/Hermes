using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elyon
{
    /// <summary>
    /// EXTRACT: Section 3
    /// </summary>
    public class IODevice
    {
        public IOQuery Query;
        public SerialCom SerialCom;
        public EventHandler<QueryCompletedArgs> QueryCompleted;

        public IODevice(string comport)
        {
            SerialCom = new SerialCom(comport);
            SerialCom.DataReceived += OnDataReceived;
            SerialCom.OpenCommunication();
        }

        #region Methods
        public bool RequestData(IOQuery query)
        {
            Query = query;
            return SerialCom.GetData(query.Command);
        }
        #endregion

        #region Events
        public void OnDataReceived(object sender, DataReceivedArgs res)
        {
            Console.WriteLine("Readived data!");

            if (res != null)
                Query.Response = res.Response;

            OnQueryCompleted();
        }

        public void OnQueryCompleted()
        {
            if(QueryCompleted != null)
                QueryCompleted(this, new QueryCompletedArgs() { Query = Query});
        }
        #endregion

    }

    public class QueryCompletedArgs : EventArgs
    {
        public IOQuery Query { get; set; }
    }
}
