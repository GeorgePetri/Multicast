namespace Multicast.Domain.Models;

// Other properties can be added here such as Topic, but for the sake of simplicity, we only need the URL
public readonly record struct Subscription(string Url);