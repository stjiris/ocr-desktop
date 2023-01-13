using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Tesseract_UI_Tools
{
    public enum QueueItemStatus
    {
        [EnumMember(Value="Created")]
        CREATED,
        [EnumMember(Value="Running")]
        RUNNING,
        [EnumMember(Value="Finished")]
        FINISHED

    }

    class QueueItem : INotifyPropertyChanged
    {
        [Display(Order = 0)]
        [DisplayName("Time Created")]
        public DateTime CreatedTime { get; private set; }

        [Display(Order = 1)]
        [DisplayName("Input Folder")]
        public string Folder { get => TesseractParams.InputFolder; }

        [Browsable(false)]
        public TesseractUIParameters TesseractParams { get; private set; }

        [Display(Order = 2)]
        [DisplayName("Status")]
        public QueueItemStatus Status{ get; private set; }

        public QueueItem(TesseractUIParameters tesseractParams)
        {
            TesseractParams = tesseractParams;
            CreatedTime = DateTime.Now;
            Status = QueueItemStatus.CREATED;
        }

        public void Update( QueueItemStatus progress )
        {
            if( Status.Equals(progress) ) return;
            Status = progress;
            if (PropertyChanged != null )
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
