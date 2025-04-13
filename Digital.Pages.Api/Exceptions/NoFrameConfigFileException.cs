using Digital.Lib.Net.Core.Exceptions;

namespace Digital.Pages.Api.Exceptions;

public class NoFrameConfigFileException(
    int id
) : DigitalException($"Frame config id:{id} does not has any file associated with it.");