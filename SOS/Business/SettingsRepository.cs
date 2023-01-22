using SOS.Constants;
using SOS.Models;
using SQLite;

namespace SOS.Business
{
    public class SettingsRepository
    {
        private SQLiteAsyncConnection conn;

        public async Task Init()
        {
            if (conn != null)
            {
                return;
            }

            conn = new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
            await conn.CreateTableAsync<SettingsData>();
        }

        public async Task<DbResponse> SaveNumber(string phoneNumber)
        {
            try
            {
                await Init();

                var settingsList = await conn.Table<SettingsData>().ToListAsync();
                var settings = settingsList.FirstOrDefault();
                if (settings is null)
                {
                    settings = new SettingsData { PhoneNumber = phoneNumber };
                    await conn.InsertAsync(settings);
                }
                else
                {
                    settings.PhoneNumber = phoneNumber;
                    await conn.UpdateAsync(settings);
                }
                return new DbResponse()
                {
                    Status = true,
                    StatusMessage = "Number saved sucessfully",
                    SettingsData = settings
                };
            }
            catch (Exception ex)
            {
                return new DbResponse()
                {
                    Status = false,
                    StatusMessage = ex.Message,
                    SettingsData = null
                };
            }
        }

        public async Task<DbResponse> IsNumberSavedAsync()
        {
            await Init();
            var settingsList = await conn.Table<SettingsData>().ToListAsync();
            var settings = settingsList.FirstOrDefault();

            if (settings is null)
            {
                return new DbResponse()
                {
                    Status = false,
                    StatusMessage = "SOS number missing",
                    SettingsData = null
                };
            }

            return new DbResponse()
            {
                Status = true,
                StatusMessage = "SOS number exists",
                SettingsData = settings
            };
        }
    }
}
