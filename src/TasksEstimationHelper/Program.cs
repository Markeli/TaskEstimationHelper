// сформируем бутстрапер и запустим выгрузку

using TasksEstimationHelper.Startup;

var bootstrapper = new AppBootstrapper();
return await bootstrapper.RunAsync(args);
