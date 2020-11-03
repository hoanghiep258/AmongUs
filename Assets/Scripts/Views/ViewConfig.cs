
public class ViewConfig
{
    public static ViewIndex[] viewIndices = { ViewIndex.EmptyView, ViewIndex.LoadingView, ViewIndex.HomeView,
        ViewIndex.GameplayView };
}

public enum ViewIndex
{
    EmptyView = 0,
    LoadingView = 1,
    HomeView = 2,
    GameplayView = 3
}

public class ViewParam
{

}
