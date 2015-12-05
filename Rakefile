require 'albacore'

build_mode = ENV['CONFIGURATION'] || 'Release'

task :default => [:compile, :test]

desc 'Compile all projects'
build :compile do |b|
  b.sln = 'chess.sln'
  b.target = ['Clean', 'Rebuild']
  b.prop 'Configuration', build_mode
  b.prop 'VisualStudioVersion', '12.0'
  b.be_quiet
  b.nologo
end

desc 'Run all unit test assemblies'
test_runner :test => [:compile] do |t|
  t.files = FileList["test/**/bin/#{build_mode}/*.Test.dll"]
  t.exe = 'packages/NUnit.Runners.2.6.4/tools/nunit-console.exe'
  t.add_parameter '/config:Release'
  t.add_parameter '/nologo'
  t.add_parameter '/noresult'
  t.add_parameter '/nodots'
end

desc 'Restore nuget packages for all projects'
nugets_restore :restore do |p|
  p.out = 'packages'
  p.exe = '.nuget/NuGet.exe'
end