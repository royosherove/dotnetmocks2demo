//using System.IO;

//namespace Step1Mocks.Inheritance
//{
//    public interface ICustomLogger
//    {
//        void Write(LogMessage msg);
//        void SetTarget(string locationOfFileOrService);
//        void Enable();
//        void Disable();

//    }

//    class CsvLogger : ICustomLogger
//    {
//        private string target;

//        public void Write(LogMessage msg)
//        {
//            var stream = File.AppendText(target);
//            using (stream)
//            {
//                stream.WriteLine(string.Format("{0}\t{1}", msg.Text, msg.Severity));
//            }
//        }

//        public void SetTarget(string locationOfFileOrService)
//        {
//            target =
//            locationOfFileOrService;
//        }

//        public void Enable()
//        {
            
//        }

//        public void Disable()
//        {
           
//        }
//    }

//    internal class LogMessage   
//    {
//        public string Text { get; set; }

//        public string Severity { get; set; }
//    }
//}