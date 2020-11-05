public class DialogConfig
{
    public static DialogIndex[] dialogIndices = { DialogIndex.InfoDialog, DialogIndex.UpgradeDialog, DialogIndex.GameOverDialog, DialogIndex.PauseDialog };
}

public enum DialogIndex
{
    InfoDialog = 0,
    UpgradeDialog = 1,
    PauseDialog = 2,
    GameOverDialog = 3
}

public class DialogParam
{

}

public class GameOverDialogParam : DialogParam
{
    public int valueKill;
    public int valueCoin;
}

public class PauseDialogParam : DialogParam
{
    public int valueKill;
    public int valueCoin;
    public float percentHP;    
}