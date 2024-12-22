public static class LocationLogic
{
    static public void WriteLocation(LocationModel location)
    {
        LocationAccess.Write(location);
    }
}