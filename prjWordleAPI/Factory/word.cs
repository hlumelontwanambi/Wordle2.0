namespace prjWordleAPI.Factory
{
    public class word
    {

        private static word instance;
        private word() { }

        public static word getInstance()
        {
            if (instance == null)
            {
                instance = new word();
            }
            return instance;
        }
        public String Random(String[] arrWords)
        {
            Random rnd = new Random();
            return arrWords[rnd.Next(arrWords.Length)];
        }
    }
}
       
        
