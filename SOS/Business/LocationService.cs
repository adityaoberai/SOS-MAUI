namespace SOS.Business
{
    public class LocationService
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task<Dictionary<string, object>> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best);

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    _isCheckingLocation = false;
                    return new()
                {
                    { "found", true },
                    { "latitude", location.Latitude.ToString() },
                    { "longitude", location.Longitude.ToString() }
                };

                }
            }
            catch (Exception ex)
            {
                return new()
                {
                    { "found", false },
                    { "error", ex.Message }
                };
            }

            return new()
            {
                { "found", false }
            };
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
