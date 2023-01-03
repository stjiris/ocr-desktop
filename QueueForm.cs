using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tesseract_UI_Tools
{
    public partial class QueueForm : Form
    {
        BindingList<QueueItem> queue = new BindingList<QueueItem>();
        public QueueForm()
        {
            InitializeComponent();
            listBox1.DataSource = queue;
            listBox1.DisplayMember = "tesseractParams";
        }

        private void addJobBtn_Click(object sender, EventArgs e)
        {
            var tesseractForm = new TesseractForm();
            DialogResult r = tesseractForm.ShowDialog();
            if( r == DialogResult.OK )
            {
                queue.Add(new QueueItem(tesseractForm.TessParams));
            }
        }
    }

    enum QueueItemStatus
    {
        READY,
        RUNNING,
        STOPPED,
        FINISHED
    }

    class QueueItem : Control, INotifyPropertyChanged
    {
        public TesseractUIParameters tesseractParams;
        DateTime queued;
        QueueItemStatus queueItemStatus;

        public QueueItem(TesseractUIParameters tesseractParams)
        {
            this.tesseractParams = tesseractParams;
            queued = DateTime.Now;
            queueItemStatus = QueueItemStatus.READY;

            var lbl = new Label();
            lbl.Text = $"{queued} - {tesseractParams.InputFolder} - {queueItemStatus}";
            this.Controls.Add(lbl);
        }

        void SetStatus( QueueItemStatus status)
        {
            if (queueItemStatus == status) return;
            queueItemStatus = status;
            if (PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
