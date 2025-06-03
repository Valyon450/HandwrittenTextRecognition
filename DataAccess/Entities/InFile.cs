using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class InFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public FileInfo ScanInfo
        {
            get
            {
                return new FileInfo(FileName);
            }                
        }
        public DateTime ImportTime { get; set; }
        public Guid? USRSId { get; set; }
        public USRS? USRS { get; set; }
        public Guid? TuningId { get; set; }
        public Tuning? Tuning { get; set; }
        public ICollection<TextBlock>? TextBlocks { get; set; }        
    }
}
