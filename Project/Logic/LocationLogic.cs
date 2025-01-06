public static class LocationLogic
{
    static public void WriteLocation(LocationModel location)
    {
        if (location.Address == "")
        {
            throw new InvalidOperationException("Invalid address.");
        }
        LocationAccess.Write(location);
    }
    static public List<LocationModel> GetAllLocations()
    {
        return LocationAccess.GetAllLocations();
    }
}