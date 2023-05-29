# MassTransit RabbitMQ sample

Based on https://masstransit.io/quick-starts/rabbitmq.

## Quick start

```bash
$ docker compose up
```

You can also look at the RabbitMQ dashboard via http://localhost:15672/ using:

- Username: `guest`
- Password: `guest`

## Known issues

The dotnet application is starting before the RabbitMQ container is finished setting up, causing an initial connection error.

After a few seconds, the dotnet application retries to connect and everything works fine from then on.
