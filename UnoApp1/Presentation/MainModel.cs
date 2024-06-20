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

    IImmutableList<Record> _collection = ImmutableList.Create(
            [
                new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(21), EndTime = DateTime.Now },
                new Record { Id = 0, StartTime = DateTime.Now - TimeSpan.FromSeconds(51), EndTime = DateTime.Now },
            ]
    );

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public IListState<Record> Records => ListState<Record>.Async(this, async (ct) => await GetRecordsAsync(ct));

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));
    }

    public Task<IImmutableList<Record>> GetRecordsAsync(CancellationToken ct)
    {
        return Task.Factory.StartNew(() => _collection);
    }

    public async Task RemoveItemAsync(Record record)
    {
        await RemoveItemAsync(record, CancellationToken.None);
    }

    private async Task RemoveItemAsync(Record record, CancellationToken ct)
    {
        _collection = _collection.Remove(record);

        var records = await GetRecordsAsync(ct);
        await Records.UpdateAsync((_) => records, ct);
    }

}
