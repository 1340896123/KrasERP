//using KrasERP.Core.EQL;

//SQLInterpreter.Interpret("");

using AutoMapper;
using AutoMapper.Data;
using Furion.DatabaseAccessor;
using Newtonsoft.Json.Linq;
using System.Data;



var host = Serve.Run(RunOptions.Default.WithArgs(args)
    // .ConfigureServices(services => services.AddScoped<ISqlRepository, SqlRepository>())
    .ConfigureServices(l => l.AddBootstrapBlazor())
    );



//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddDataReaderMapping(); // ���ö�DataReader��ӳ��֧��
//    cfg.CreateMap<IDataRecord, JObject>(); // ���ô�IDataRecord��Person��ӳ��
//});

//// ����һ��IMapperʵ��
//IMapper mapper = config.CreateMapper();