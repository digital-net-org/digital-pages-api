using Digital.Lib.Net.Core.Exceptions;

namespace Digital.Pages.Api.Exceptions;

public class NoPublishedConfigFileException(
    int id
) : DigitalException($"Published frame config id:{id} does not has any file associated with it.");