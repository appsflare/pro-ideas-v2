namespace ProIdeas.DataMappings.Data.Mappings.Contracts
{
    public interface IDataMapper
    {
        //
        // Summary:
        //     Execute a mapping from the source object to a new destination object. The source
        //     type is inferred from the source object.
        //
        // Parameters:
        //   source:
        //     Source object to map from
        //
        // Type parameters:
        //   TDestination:
        //     Destination type to create
        //
        // Returns:
        //     Mapped destination object
        TDestination Map<TDestination>(object source);
    }
}
