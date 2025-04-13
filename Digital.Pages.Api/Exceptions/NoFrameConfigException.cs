using Digital.Lib.Net.Core.Exceptions;

namespace Digital.Pages.Api.Exceptions;

public class NoFrameConfigException() : DigitalException($"No Frame config available. Please upload a config file.");