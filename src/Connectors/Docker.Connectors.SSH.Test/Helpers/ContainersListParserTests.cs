using Docker.Connectors.SSH.Helpers;

namespace Docker.Connectors.SSH.Test.Helpers;

public class ContainersListParserTests
{
    private const string RawResultMultiple =
        "{\"Command\":\"\\\"docker-entrypoint.s…\\\"\",\"CreatedAt\":\"2023-11-28 18:35:53 +0330 +0330\",\"ID\":\"84117b05be31\",\"Image\":\"redis:latest\",\"Labels\":\"desktop.docker.io/wsl-distro=Ubuntu\",\"LocalVolumes\":\"1\",\"Mounts\":\"549a979e6fd8d1…\",\"Names\":\"alaki\",\"Networks\":\"bridge\",\"Ports\":\"\",\"RunningFor\":\"12 minutes ago\",\"Size\":\"0B\",\"State\":\"exited\",\"Status\":\"Exited (1) 12 minutes ago\"}\n{\"Command\":\"\\\"docker-entrypoint.s…\\\"\",\"CreatedAt\":\"2023-11-26 17:33:49 +0330 +0330\",\"ID\":\"5f3dbb65323c\",\"Image\":\"redis:latest\",\"Labels\":\"com.docker.compose.service=db,com.docker.compose.version=2.23.0,com.docker.compose.image=sha256:961dda256baa7a35297d34cca06bc2bce8397b0ef8b68d8064c30e338afc5a7d,com.docker.compose.oneoff=False,com.docker.compose.project.config_files=/mnt/d/dockers/redis/docker-compose.yml,com.docker.compose.project.working_dir=/mnt/d/dockers/redis,com.docker.compose.container-number=1,desktop.docker.io/binds/0/SourceKind=hostFile,desktop.docker.io/binds/0/Target=/data,desktop.docker.io/wsl-distro=Ubuntu,com.docker.compose.config-hash=52127b472ea2623b7142ccc71d5634878f1ae11fbe47117d2038fe17379d14de,com.docker.compose.project=redis,desktop.docker.io/binds/0/Source=/mnt/d/dockers/redis/data,com.docker.compose.depends_on=\",\"LocalVolumes\":\"0\",\"Mounts\":\"/run/desktop/m…\",\"Names\":\"redis\",\"Networks\":\"redis_default\",\"Ports\":\"0.0.0.0:6379->6379/tcp, 0.0.0.0:6666->7777/tcp\",\"RunningFor\":\"2 days ago\",\"Size\":\"0B\",\"State\":\"exited\",\"Status\":\"Exited (255) About an hour ago\"}\n{\"Command\":\"\\\"/init\\\"\",\"CreatedAt\":\"2023-10-12 22:25:49 +0330 +0330\",\"ID\":\"0ebda6ef1109\",\"Image\":\"emby/embyserver:latest\",\"Labels\":\"com.docker.compose.container-number=1,com.docker.compose.image=sha256:2552cbe87d6db5ffe84a9d2053b1728905be28a82b0effd114bdc1897eaa362b,com.docker.compose.oneoff=False,com.docker.compose.project.config_files=/home/naser/dockers/emby/docker-compose.yml,com.docker.compose.project.working_dir=/home/naser/dockers/emby,com.docker.compose.version=2.21.0,com.docker.compose.config-hash=7bde31b5e030b18f41fb9cdc0c398e9f90650ef70d07e2391a8c24f88bbb161b,com.docker.compose.project=emby,com.docker.compose.service=emby,maintainer=Emby LLC <apps@emby.media>,com.docker.compose.depends_on=\",\"LocalVolumes\":\"0\",\"Mounts\":\"/home/naser/do…,/media/disk700…,/media/disk700…,/media/disk700…,/media/disk700…\",\"Names\":\"embyserver\",\"Networks\":\"host\",\"Ports\":\"\",\"RunningFor\":\"7 weeks ago\",\"Size\":\"6.53kB (virtual 571MB)\",\"State\":\"running\",\"Status\":\"Up 2 days\"}";
    private const string RawResultSingle =
        "{\"Command\":\"\\\"docker-entrypoint.s…\\\"\",\"CreatedAt\":\"2023-11-28 18:35:53 +0330 +0330\",\"ID\":\"84117b05be31\",\"Image\":\"redis:latest\",\"Labels\":\"desktop.docker.io/wsl-distro=Ubuntu\",\"LocalVolumes\":\"1\",\"Mounts\":\"549a979e6fd8d1…\",\"Names\":\"alaki\",\"Networks\":\"bridge\",\"Ports\":\"\",\"RunningFor\":\"12 minutes ago\",\"Size\":\"0B\",\"State\":\"exited\",\"Status\":\"Exited (1) 12 minutes ago\"}";

    [Fact]
    public void Parse_Return_Multiple_Success()
    {
        //arrange

        //act
        var convertor = ContainersParser.List(RawResultMultiple);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 3);
        Assert.Equal("redis:latest", convertor[0].Image);
        Assert.NotEmpty(convertor[0].Labels);
        Assert.NotEmpty(convertor[1].Ports);
        Assert.Equal(28, convertor[0].CreatedAt.Day);
        Assert.Equal("bridge", convertor[0].Networks[0]);
    }    
    
    [Fact]
    public void Parse_Return_Single_Success()
    {
        //arrange

        //act
        var convertor = ContainersParser.List(RawResultSingle);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 1);
        Assert.Equal("redis:latest", convertor[0].Image);
        Assert.NotEmpty(convertor[0].Labels);
        Assert.Equal(28, convertor[0].CreatedAt.Day);
        Assert.Equal("bridge", convertor[0].Networks[0]);
    }
    
    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange
        const string emptyList = "";

        //act
        var convertor = ContainersParser.List(emptyList);

        //assert
        Assert.Empty(convertor);
    }
}