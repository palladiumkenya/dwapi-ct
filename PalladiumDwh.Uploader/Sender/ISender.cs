using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PalladiumDwh.Uploader.Model;

namespace PalladiumDwh.Uploader.Sender
{
    public interface ISender<in TEntity>
    {
         Task PostEntity(TEntity t);
         Task CreateEntity(DataGridView gridView, TEntity t,IProgress<decimal> progress);

    }
}
