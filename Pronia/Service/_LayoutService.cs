namespace Pronia.Service
{
    public class _LayoutService
    {
        AppDbContext _context;
        public _LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSetting()
        {
            Dictionary<string, string> setting = _context.Setting.ToDictionary(s => s.Key, s => s.Value);
            return setting;

        }

    }
}
