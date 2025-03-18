using Digital.Lib.Net.Core.Exceptions;

namespace Digital.Pages.Api.Exceptions;

public class NoPublishedConfigException() : DigitalException("No published frame config found.");