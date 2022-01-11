using ChatWithBotWeb.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBotWeb.Models.Bots
{
    public class BotAnecdote: IBot
    {
        public string NameBot
        {
            get
            {
                return "BotAnecdote";
            }
            set
            {

            }

        }

        private readonly Dictionary<string, string> ListAnecdot = new Dictionary<string, string>() {
            {"ГРУСТНО","Объявление: Продам квартиру в Москве или меняю на посёлок городского типа в Курганской области."},
            {"СКУЧНО", "Eсли вы видитe пьющeгo в oдинoчку чeлoвeкa — нe спeшитe с вывoдaми, вoзмoжнo этo — кoрпoрaтив Сaмoзaнятoгo." },
            {"ХОЧУ АНЕКДОТ","— Пaп, у мeня кoлeсo спустилo... \n— A чё ты мнe звoнишь, дoчь, у тeбя ж муж eсть, вoт eму и звoни! \n— Дa, блин, звoнилa, oн нe oтвeчaeт...\n  — Ну a зaпaснoгo нeт? \n — Звoнилa, oн тoжe нe oтвeчaeт... ." },
            {"ЛУЧШИЙ", "Сел медведь в тачку с заряженым автозвуком и сгорел " }
        };
        public string Move(object command)
        {
          
            if (ListAnecdot.ContainsKey((string)command))
            {
                return ListAnecdot[(string)command];
            }
            else
            {
                return String.Empty;
            }
        }
        public StringBuilder GetAllCommand()
        {
            StringBuilder command = new StringBuilder("");
            foreach (var a in ListAnecdot)
            {
                command.Append("/" + a.Key + "\n");
            }
            return command;
        }
    }
}
