using Games_Func.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Func.Domain.DataAccess
{
    public class RecordRepository : IRecordRepository
    {

        Contexto contexto;
        public RecordRepository()
        {
            contexto = new Contexto();

        }
        public void Limpar(string game)
        {
            var range = contexto.Records.Where(p => p.Id > 1 && p.Game.Equals(game)).ToArray();
            contexto.Records.RemoveRange(range);
            contexto.SaveChanges();
        }

        public int ObterMin(string game)
        {
            if (contexto.Records.Count() == 0)
                return 0;

            return contexto.Records.Where(p => p.Game.Equals(game)).Min(x => x.Pontos);
        }

        public Record ObterPor(int id)
        {
            return contexto.Records.FirstOrDefault(p => p.Id == id);
        }

        public List<Record> ObterTodos(string game)
        {
            return contexto.Records.Where(p => p.Game.Equals(game)).AsNoTracking().ToList();
        }

        public void Remover(int id)
        {
            var r = ObterPor(id);
            contexto.Records.Remove(r);
            contexto.SaveChanges();
        }

        public void Salvar(Record entity)
        {
            if (entity.Id == 0)
                contexto.Records.Add(entity);

            ManterTop10(entity.Game);

            contexto.SaveChanges();
        }

        private void ManterTop10(string game)
        {
            if (contexto.Records.Where(p => p.Game == game).Count() < 9) return;

            int min = ObterMin(game);
            Record item = contexto.Records.Where(p => p.Game == game && p.Pontos == min).FirstOrDefault();
            if (item != null)
                contexto.Records.Remove(item);
            contexto.SaveChanges();


        }
    }
}
