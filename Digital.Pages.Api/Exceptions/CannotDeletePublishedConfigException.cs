using Digital.Lib.Net.Core.Exceptions;

namespace Digital.Pages.Api.Exceptions;

public class CannotDeletePublishedConfigException(
    int id
) : DigitalException($"Published frame config with ID {id} cannot be deleted. Please change the published config for each concerned frame first.");