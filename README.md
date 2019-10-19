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