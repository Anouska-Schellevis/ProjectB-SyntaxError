public static class LocationLogic
{
    static public void WriteLocation(LocationModel location)
    {
        LocationAccess.Write(location);
    }
    static public List<LocationModel> GetAllLocations()
    {
        return LocationAccess.GetAllLocations();
    }
}