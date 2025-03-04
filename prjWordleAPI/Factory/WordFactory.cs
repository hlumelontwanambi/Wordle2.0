namespace prjWordleAPI.Factory
{
    public class WordFactory
    {
        public iWord GetWord(string type)
        {
            if (type == "generate")
            {
                return new GenerateWord();
            }
            else if (type == "check")
            {
                return new CheckWord();
            }
            else
            {
                throw new ArgumentException("Invalid word type.");
            }
        }
    }
}
