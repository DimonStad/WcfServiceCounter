using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WcfServiceCounter
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        
        public Dictionary<string, int> GetCounter(string str)
        {
            string signs = "<>.,?!-/*”“'\"[]{}";
            foreach (char c in signs)
            {
                str = str.Replace(c, ' ');
            }
            str = str.Replace(Environment.NewLine, " ");
            string[] arr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var wordCount = new Dictionary<string, int>();
            var result = new System.Collections.Concurrent.ConcurrentDictionary<string, int>();
            Parallel.ForEach(arr, line =>
            {
                result.AddOrUpdate(line, 1, (_, x) => x + 1);

            });
            var ordered = result.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }

    }
}
