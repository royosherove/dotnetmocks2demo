using System.IO;

namespace ProductinWithInheritance
{
    public class CsvLogger : ICustomLogger
    {
        private string target;

        public void Write(LogMessage msg)
        {
            var stream = File.AppendText(target);
            using (stream)
            {
                stream.WriteLine(string.Format("{0}\t{1}", msg.Text, msg.Severity));
            }
        }

        public void SetTarget(string locationOfFileOrService)
        {
            target =
                locationOfFileOrService;
        }

        public void Enable()
        {

        }

        public void Disable()
        {

        }
    }
}