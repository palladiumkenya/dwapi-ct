using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PalladiumDwh.Uploader.Sender
{
    public interface ISender<in TEntity>
    {
         Task PostEntity(TEntity t);
         Task CreateEntity(DataGridView gridView, TEntity t,IProgress<decimal> progress);

    }
}
