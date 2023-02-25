import { getNodeAutoInstrumentations } from '@opentelemetry/auto-instrumentations-node';
import { Resource } from '@opentelemetry/resources';
import * as opentelemetry from '@opentelemetry/sdk-node';
import { SemanticResourceAttributes } from '@opentelemetry/semantic-conventions';
import { v4 as uuidv4 } from 'uuid';
import {diag, DiagConsoleLogger, DiagLogLevel} from '@opentelemetry/api'
import { JaegerExporter } from '@opentelemetry/exporter-jaeger';
import {  } from '@opentelemetry/sdk-trace-node';
import { HttpInstrumentation } from '@opentelemetry/instrumentation-http';
import { ExpressInstrumentation } from '@opentelemetry/instrumentation-express';
import { SimpleSpanProcessor } from '@opentelemetry/sdk-trace-base';

diag.setLogger(
  new DiagConsoleLogger(),
  DiagLogLevel.DEBUG
);

const exporter = new JaegerExporter({
  endpoint: "http://172.17.0.1:14268/api/traces",
});

const OTEL_SERVICE_RESOURCE = new Resource({
  [SemanticResourceAttributes.SERVICE_NAME]: 'super-api',
  [SemanticResourceAttributes.SERVICE_VERSION]: '1.0.0',
  [SemanticResourceAttributes.SERVICE_INSTANCE_ID]: uuidv4()
});

const expressInstrumentation = new ExpressInstrumentation();

const sdk = new opentelemetry.NodeSDK({
  instrumentations: [getNodeAutoInstrumentations(), new HttpInstrumentation(), expressInstrumentation],
  resource: OTEL_SERVICE_RESOURCE,
})
sdk.configureTracerProvider({}, new SimpleSpanProcessor(exporter))
sdk.start()
