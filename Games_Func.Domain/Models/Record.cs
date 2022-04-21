using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Func.Domain.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string Game { get; set; }
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public string TempoDeJogo { get; set; }
        public string Data { get; set; }
    }

    public interface IRecordRepository
    {
        void Salvar(Record entity);
        void Remover(int id);
        Record ObterPor(int id);
        List<Record> ObterTodos(string game);
        int ObterMin(string game);
        void Limpar(string game);
    }
}
