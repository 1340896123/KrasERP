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
//    cfg.AddDataReaderMapping(); // 启用对DataReader的映射支持
//    cfg.CreateMap<IDataRecord, JObject>(); // 配置从IDataRecord到Person的映射
//});

//// 创建一个IMapper实例
//IMapper mapper = config.CreateMapper();