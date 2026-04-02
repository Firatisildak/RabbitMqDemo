# RabbitMQ Order Processing Demo (.NET 8)

Bu proje, .NET 8 ile geliştirilmiş basit bir **event-driven architecture** örneğidir. API, siparişleri alır ve RabbitMQ kuyruğuna gönderir; arka planda çalışan Worker bu kuyruğu dinleyip siparişleri işler.

## Teknolojiler
- .NET 8 Web API  
- Worker Service  
- RabbitMQ (Docker)  
- JSON Serialization  

## Kurulum

1. Docker yüklü değilse: https://www.docker.com/products/docker-desktop/  
2. RabbitMQ başlat:  
```bash
docker run -d --hostname rabbit-host --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
