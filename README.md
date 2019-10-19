IPeerIdentification is used for identifying distinct users. There are two built in implementations, one with string (shown below) and one with integer.

IPeerManager manages the mapping of IPeerIdentifications and IPEndPoints. When a message from an unknown peer is received, it will add the IPEndPoint.

ISerialiser is a simple interface used to convert message between classes and byte arrays, Json.Net implementation is added by default, but any serialiser can be used.

UDPNetworking uses an event called OnMessageReceived to notify of new messages, and message can be sent asynchronously by using SendMessageAsync.

```csharp
const int servicePort = 1337;

IPeerIdentification peerAId = new StringPeerIdentification("PeerA");
IPeerIdentification peerBId = new StringPeerIdentification("PeerB");

IPeerManager peerManager = new PeerManager();
peerManager.AddOrUpdatePeer(peerBId, new IPEndPoint(IPAddress.Parse("peerb.example.com"), servicePort));

IUDPNetworking peerA = new UDPNetworking(
	peerAId,
	peerManager,
	new JSONSerialiser(),
	new IPEndPoint(IPAddress.Any, servicePort)
);

peerA.OnMessageReceived += (sender, message) => Console.WriteLine(((StringMessage) message).Data);

peerA.SendMessageAsync(new StringMessage()
{
	To = peerBId,
	From = peerAId,
	Data = "Hello B!"
});
```