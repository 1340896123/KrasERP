//using KrasERP.Core.EQL;

//SQLInterpreter.Interpret("");

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