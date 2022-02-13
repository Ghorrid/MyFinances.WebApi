namespace MyFinances.Core.Response
{
    public class Response
    {
        public Response()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }
        public bool IsSuccess
        {
            get { return Errors == null || !Errors.Any(); }
        } 
        
        // => Errors != null || !Errors.Any();

    }
}
