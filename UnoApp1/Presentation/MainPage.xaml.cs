namespace UnoApp1.Presentation;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void RemoveItem_Invoked(SwipeItem _, SwipeItemInvokedEventArgs args)
    {
        var viewModel = this.DataContext;
        var swipedItem = args.SwipeControl.DataContext;

        var propertyInfo = viewModel.GetType().GetProperties().FirstOrDefault(x => x.Name == "RemoveItemAsync");
        if (propertyInfo != null)
        {
            var asyncCommand = propertyInfo.GetValue(viewModel) as IAsyncCommand;
            if (asyncCommand?.CanExecute(swipedItem) ?? false)
            {
                asyncCommand.Execute(swipedItem);
            }
        }
    }
}
