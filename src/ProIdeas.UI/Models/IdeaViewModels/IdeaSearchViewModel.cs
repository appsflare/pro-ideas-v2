namespace ProIdeas.UI.Models.IdeaViewModels
{
    public class IdeaSearchViewModel
    {

        private IdeaSearchViewModel(string keyword)
        {
            Keyword = keyword;
        }

        public string Keyword { get; private set; }

        public static IdeaSearchViewModel CreateWithKeyword(string keyword)
        {
            return new IdeaSearchViewModel(keyword);
        }


    }
}
