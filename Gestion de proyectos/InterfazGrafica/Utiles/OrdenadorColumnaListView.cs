using System;

namespace InterfazGrafica.Utiles
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;


    public class OrdenadorColumnaListView : IComparer
    {
       
        private int ColumnaAOrdenar;
  
        private SortOrder OrdenDeOrdenamiento;

        private CaseInsensitiveComparer ComparadorDeObjetos;

        public OrdenadorColumnaListView()
        {
 
            ColumnaAOrdenar = 0;

            OrdenDeOrdenamiento = SortOrder.None;
            ComparadorDeObjetos = new CaseInsensitiveComparer();
        }

        public int Compare(object comparador, object comparando)
        {
            int resultadoComparacion;
            ListViewItem comparadorDeListView, comparandoDeListView;

            comparadorDeListView = (ListViewItem)comparador;
            comparandoDeListView = (ListViewItem)comparando;

            resultadoComparacion = CompararObjetosPorTipoColumna(comparadorDeListView, comparandoDeListView);

            if (OrdenDeOrdenamiento == SortOrder.Ascending)
            {
                return resultadoComparacion;
            }
            else if (OrdenDeOrdenamiento == SortOrder.Descending)
            {
                return (-resultadoComparacion);
            }
            else
            {
                return 0;
            }
        }

        private int CompararObjetosPorTipoColumna(ListViewItem comparador, ListViewItem comparando)
        {
            if (comparador.SubItems[ColumnaAOrdenar].Tag != null)
            {
                if (comparador.SubItems[ColumnaAOrdenar].Tag.Equals("int"))
                {
                    return Int32.Parse(comparador.SubItems[ColumnaAOrdenar].Text).CompareTo(Int32.Parse(comparando.SubItems[ColumnaAOrdenar].Text));
                }
                else if (comparador.SubItems[ColumnaAOrdenar].Tag.Equals("string"))
                {
                    return String.Compare(comparador.SubItems[ColumnaAOrdenar].Text, comparando.SubItems[ColumnaAOrdenar].Text);
                }
                else if (comparador.SubItems[ColumnaAOrdenar].Tag.Equals("DateTime"))
                {
                    return DateTime.Compare(CrearDateTimeDesdeString(comparador.SubItems[ColumnaAOrdenar].Text),
                        CrearDateTimeDesdeString(comparando.SubItems[ColumnaAOrdenar].Text));
                }
            }
                return ComparadorDeObjetos.Compare(comparador.SubItems[ColumnaAOrdenar].Text, comparando.SubItems[ColumnaAOrdenar].Text);
        }

        private DateTime CrearDateTimeDesdeString(string fecha)
        {
            if(fecha.Equals(String.Empty))
                return DateTime.MaxValue;
            return DateTime.Parse(fecha);
        }

        public int OrdenarColumna
        {
            set
            {
                ColumnaAOrdenar = value;
            }
            get
            {
                return ColumnaAOrdenar;
            }
        }

        public SortOrder Orden
        {
            set
            {
                OrdenDeOrdenamiento = value;
            }
            get
            {
                return OrdenDeOrdenamiento;
            }
        }

    }

    internal static class ListViewExtensions
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LVCOLUMN
        {
            public Int32 mask;
            public Int32 cx;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public IntPtr hbm;
            public Int32 cchTextMax;
            public Int32 fmt;
            public Int32 iSubItem;
            public Int32 iImage;
            public Int32 iOrder;
        }

        const Int32 HDI_WIDTH = 0x0001;
        const Int32 HDI_HEIGHT = HDI_WIDTH;
        const Int32 HDI_TEXT = 0x0002;
        const Int32 HDI_FORMAT = 0x0004;
        const Int32 HDI_LPARAM = 0x0008;
        const Int32 HDI_BITMAP = 0x0010;
        const Int32 HDI_IMAGE = 0x0020;
        const Int32 HDI_DI_SETITEM = 0x0040;
        const Int32 HDI_ORDER = 0x0080;
        const Int32 HDI_FILTER = 0x0100;

        const Int32 HDF_LEFT = 0x0000;
        const Int32 HDF_RIGHT = 0x0001;
        const Int32 HDF_CENTER = 0x0002;
        const Int32 HDF_JUSTIFYMASK = 0x0003;
        const Int32 HDF_RTLREADING = 0x0004;
        const Int32 HDF_OWNERDRAW = 0x8000;
        const Int32 HDF_STRING = 0x4000;
        const Int32 HDF_BITMAP = 0x2000;
        const Int32 HDF_BITMAP_ON_RIGHT = 0x1000;
        const Int32 HDF_IMAGE = 0x0800;
        const Int32 HDF_SORTUP = 0x0400;
        const Int32 HDF_SORTDOWN = 0x0200;

        const Int32 LVM_FIRST = 0x1000;
        const Int32 LVM_GETHEADER = LVM_FIRST + 31;
        const Int32 HDM_FIRST = 0x1200;
        const Int32 HDM_SETIMAGELIST = HDM_FIRST + 8;
        const Int32 HDM_GETIMAGELIST = HDM_FIRST + 9;
        const Int32 HDM_GETITEM = HDM_FIRST + 11;
        const Int32 HDM_SETITEM = HDM_FIRST + 12;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageLVCOLUMN(IntPtr hWnd, Int32 Msg, IntPtr wParam, ref LVCOLUMN lPLVCOLUMN);


        
        public static void AsignarIconoColumna(this ListView listView, int indiceColumna, SortOrder orden)
        {
            IntPtr cabeceraColumna = SendMessage(listView.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);

            for (int numeroColumna = 0; numeroColumna <= listView.Columns.Count - 1; numeroColumna++)
            {
                IntPtr punteroColumna = new IntPtr(numeroColumna);
                LVCOLUMN columnaListView = new LVCOLUMN();
                columnaListView.mask = HDI_FORMAT;

                SendMessageLVCOLUMN(cabeceraColumna, HDM_GETITEM, punteroColumna, ref columnaListView);

                if (!(orden == SortOrder.None) && numeroColumna == indiceColumna)
                {
                    switch (orden)
                    {
                        case System.Windows.Forms.SortOrder.Ascending:
                            columnaListView.fmt &= ~HDF_SORTDOWN;
                            columnaListView.fmt |= HDF_SORTUP;
                            break;
                        case System.Windows.Forms.SortOrder.Descending:
                            columnaListView.fmt &= ~HDF_SORTUP;
                            columnaListView.fmt |= HDF_SORTDOWN;
                            break;
                    }
                    columnaListView.fmt |= (HDF_LEFT | HDF_BITMAP_ON_RIGHT);
                }
                else
                {
                    columnaListView.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP & ~HDF_BITMAP_ON_RIGHT;
                }

                SendMessageLVCOLUMN(cabeceraColumna, HDM_SETITEM, punteroColumna, ref columnaListView);
            }
        }
    }


}
