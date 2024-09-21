

namespace Ninja.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class EchoHandler : WebSocketBehavior
    {
        WebSocketServer wsServer = null;

        public WebSocket wsClient;

        public static ObservableCollection<string> wsRecv { get; set; } = 
            new ObservableCollection<string> { };

        public static ObservableCollection<string> wsClientRecv { get; set; } = 
            new ObservableCollection<string> { };

        protected override void OnMessage(MessageEventArgs e)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                string time = "[" + this.StartTime + "][";
                string from = this.UserEndPoint.ToString();
                string str = "][" + e.Data + "]\n";
                wsRecv.Add(time + from + str);
            }));

            Send(e.Data);
        }

        protected override void OnOpen()
        {
            string time = "[" + this.StartTime + "][";
            string from = this.UserEndPoint.ToString();
            string status = "][" + ReadyState + "]\n";
            wsRecv.Add(time + from + status);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            string time = "[" + this.StartTime + "][";
            string reason = e.Reason;
            string status = "][" + ReadyState + "]\n";
            wsRecv.Add(time + reason + status);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            string time = "[" + this.StartTime + "][";
            string reason = e.Message;
            string status = "][" + ReadyState + "]\n";
            wsRecv.Add(time + reason + status);
        }
    }

}
