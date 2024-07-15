namespace UnoApp1.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    IImmutableList<FilteredRecordsGroup> _collection = ImmutableList.Create(
        [
            new FilteredRecordsGroup(
                "5 Elements",
                [
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(21), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(51), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(121), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(200), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(300), EndTime = DateTime.Now },
                ],
                true
            ),
            new FilteredRecordsGroup(
                "4 Elements",
                [
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(21), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(51), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(121), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(200), EndTime = DateTime.Now },
                ],
                false
            ),
            new FilteredRecordsGroup(
                "3 Elements",
                [
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(21), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(51), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(121), EndTime = DateTime.Now },
                ],
                false
            ),
            new FilteredRecordsGroup(
                "2 Elements",
                [
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(21), EndTime = DateTime.Now },
                    new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(51), EndTime = DateTime.Now },
                ],
                false
            ),
        ]
    );

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public IListState<FilteredRecordsGroup> Records => ListState<FilteredRecordsGroup>.Async(this, async (ct) => await GetRecordsAsync(ct));

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }

    public Task<IImmutableList<FilteredRecordsGroup>> GetRecordsAsync(CancellationToken ct)
    {
        return Task.Factory.StartNew(() => _collection);
    }

    public async Task RemoveItemAsync(Record record)
    {
        await RemoveItemAsync(record, CancellationToken.None);
    }

    private async Task RemoveItemAsync(Record record, CancellationToken ct)
    {
        await Task.CompletedTask;
    }

}
