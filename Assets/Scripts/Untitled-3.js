'use strict';

function unwrapExports (x) {
	return x && x.__esModule && Object.prototype.hasOwnProperty.call(x, 'default') ? x['default'] : x;
}

function createCommonjsModule(fn, module) {
	return module = { exports: {} }, fn(module, module.exports), module.exports;
}

var getProxyResponse_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});


/**
 * This will get the response from the proxy
 * @param  settings                   Object containing the settings for getting the response from the proxy
 * @param  settings.characterEncoding Optionally convert the response to UTF-8
 * @return                            The response from the targetted API
 */
var getProxyResponse = exports.getProxyResponse = function getProxyResponse() {
	var _ref = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : { characterEncoding: '' },
	    _ref$characterEncodin = _ref.characterEncoding,
	    characterEncoding = _ref$characterEncodin === undefined ? '' : _ref$characterEncodin;

	var proxyResponse = characterEncoding === 'UTF-8' ? unescape(encodeURIComponent(context.proxyResponse.content)) : context.proxyResponse.content;

	try {
		return JSON.parse(proxyResponse);
	} catch (e) {
		return {};
	}
};

exports.default = getProxyResponse;
});

var getProxyResponse = unwrapExports(getProxyResponse_1);
var getProxyResponse_2 = getProxyResponse_1.getProxyResponse;

var setResponse_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});


/**
 * This will set the response to the provided content
 * @param  content              The response to set for the proxy
 * @param  settings             Object containing the settings for setting the response
 * @param  settings.contentType An optional contenttype header to set for the response
 */
var setResponse = exports.setResponse = function setResponse(content) {
	var _ref = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {
		contentType: undefined
	},
	    contentType = _ref.contentType;

	context.proxyResponse.content = JSON.stringify(content);

	if (contentType) {
		context.setVariable('response.header.content-type', contentType);
	}
};

exports.default = setResponse;
});

var setResponse = unwrapExports(setResponse_1);
var setResponse_2 = setResponse_1.setResponse;

var getVariables_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});


/**
 * This will get a set of values from the Apigee flow
 * @param  keys                   The keys the values to get are stored with
 * @param  settings               Object containing the settings for getting the variables
 * @param  settings.prefix        A prefix which is used to store the value with
 * @param  settings.defaultValues The value to return when no value is found. The keys of the default values should be identical to the variable keys.
 * @param  settings.parser        The parser is an object containing functions which take a value and transforms it to return something else. The keys of the parser should be identical to the variable keys.
 * @return                        The values parsed from the apigee flow
 */
var getVariables = exports.getVariables = function getVariables(keys) {
	var _ref = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {
		prefix: '',
		defaultValues: {},
		parser: {}
	},
	    _ref$prefix = _ref.prefix,
	    prefix = _ref$prefix === undefined ? '' : _ref$prefix,
	    _ref$defaultValues = _ref.defaultValues,
	    defaultValues = _ref$defaultValues === undefined ? {} : _ref$defaultValues,
	    _ref$parser = _ref.parser,
	    parser = _ref$parser === undefined ? {} : _ref$parser;

	return keys.reduce(function (variables, key) {
		var variable = context.getVariable(prefix + key);

		if (variable !== null) {
			variables[key] = parser[key] ? parser[key](variable) : variable;
		} else if (defaultValues[key]) {
			variables[key] = defaultValues[key];
		}

		return variables;
	}, {});
};

exports.default = getVariables;
});

var getVariables = unwrapExports(getVariables_1);
var getVariables_2 = getVariables_1.getVariables;

var logMessage_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});

var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

/**
 * This will log a message to the syslog variable
 * @param additionalLogvalues Object containing additional key values to be logged
 */
var logMessage = exports.logMessage = function logMessage(additionalLogvalues) {
	var apigeeVariables = ['apiproxy.name', 'apiproxy.revision', 'client.received.start.time', 'client.received.start.timestamp', 'environment.name', 'error.content', 'error.message', 'error.state', 'log.message', 'message.reason.phrase', 'message.status.code', 'messageid', 'proxy.client.ip', 'proxy.url', 'request.url', 'response.reason.phrase', 'response.status.code', 'servicecallout.requesturi', 'target.ip', 'target.name', 'target.received.end.time', 'target.received.end.timestamp', 'target.received.start.time', 'target.received.start.timestamp', 'target.url'];
	var syslogMessage = apigeeVariables.reduce(function (logMessage, key) {
		var value = context.getVariable(key);
		var nextLogMessage = logMessage;

		key.split('.').reduce(function (nextLogMessage, part, index, path) {
			nextLogMessage[part] = index >= path.length - 1 ? value : nextLogMessage[part] || {};

			return nextLogMessage[part];
		}, nextLogMessage);

		return nextLogMessage;
	}, {});

	if (additionalLogvalues) {
		syslogMessage = _extends({}, syslogMessage, additionalLogvalues);
	}

	context.setVariable('log.syslog.message', JSON.stringify(syslogMessage));
};

exports.default = logMessage;
});

var logMessage = unwrapExports(logMessage_1);
var logMessage_2 = logMessage_1.logMessage;

var getQueryParam_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});


/**
 * This will get a queryParam from the passed url string
 * @param  queryParamKey  The key for the queryParam
 * @param  defaultValue   The default value to return when nothing is available
 * @return                The value of the queryParam
 */
var getQueryParam = exports.getQueryParam = function getQueryParam(queryParamKey, defaultValue) {
  var queryParam = context.getVariable('request.queryparam.' + queryParamKey);

  return queryParam === undefined || queryParam === null ? defaultValue : queryParam;
};

exports.default = getQueryParam;
});

unwrapExports(getQueryParam_1);
var getQueryParam_2 = getQueryParam_1.getQueryParam;

var getQueryParams_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.getQueryParams = undefined;

var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };



var _getQueryParam2 = _interopRequireDefault(getQueryParam_1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

/**
 * This will get a set of queryParams from the passed url string
 * @param  possibleQueryParams    An array containing possible queryparams
 * @param  settings               Object containing the settings for getting the queryparams
 * @param  settings.defaultValues The value to return when no value is found. The keys of the default values should be identical to the queryparam keys.
 * @return                        An object containing values for the passed in queryparams
 */
var getQueryParams = exports.getQueryParams = function getQueryParams(possibleQueryParams) {
  var _ref = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {
    defaultValues: {}
  },
      _ref$defaultValues = _ref.defaultValues,
      defaultValues = _ref$defaultValues === undefined ? {} : _ref$defaultValues;

  return possibleQueryParams.reduce(function (queryParams, possibleQueryKey) {
    return _extends({}, queryParams, _defineProperty({}, possibleQueryKey, (0, _getQueryParam2.default)(possibleQueryKey, defaultValues[possibleQueryKey])));
  }, defaultValues);
};

exports.default = getQueryParams;
});

unwrapExports(getQueryParams_1);
var getQueryParams_2 = getQueryParams_1.getQueryParams;

var setVariable_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});


/**
 * This will store a value in the Apigee flow
 * @param  key                    The key the value should be stored in
 * @param  value                  The value to store
 * @param  settings               Object containing the settings for setting the variable
 * @param  settings.prefix        A prefix which is used to store the value with
 */
var setVariable = exports.setVariable = function setVariable(key, value) {
  var _ref = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : {
    prefix: ''
  },
      _ref$prefix = _ref.prefix,
      prefix = _ref$prefix === undefined ? '' : _ref$prefix;

  if (value !== undefined) {
    context.setVariable(prefix + key, value);
  }
};

exports.default = setVariable;
});

unwrapExports(setVariable_1);
var setVariable_2 = setVariable_1.setVariable;

var setVariables_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});
exports.setVariables = undefined;



var _setVariable2 = _interopRequireDefault(setVariable_1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

/**
 * This will store a set of values in the Apigee flow
 * @param  variables              An object containing key value pairs to store
 * @param  settings               Object containing the settings for setting the variables
 * @param  settings.prefix        A prefix which is used to store the value with
 */
var setVariables = exports.setVariables = function setVariables(variables) {
	var _ref = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {
		prefix: ''
	},
	    _ref$prefix = _ref.prefix,
	    prefix = _ref$prefix === undefined ? '' : _ref$prefix;

	return Object.keys(variables).forEach(function (key) {
		if (variables[key] !== undefined && variables[key] !== null) {
			(0, _setVariable2.default)(key, variables[key], {
				prefix: prefix
			});
		}
	});
};
exports.default = setVariables;
});

unwrapExports(setVariables_1);
var setVariables_2 = setVariables_1.setVariables;

var validateValues_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});
exports.validateValues = undefined;



var _setVariables2 = _interopRequireDefault(setVariables_1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) { arr2[i] = arr[i]; } return arr2; } else { return Array.from(arr); } }

/**
 * This will validate a set of query parameters and will set a error variable in the apigee with an errorpayload variable which can be send down to the client
 * It is advised to set up a raise on error policy which will return the payload when the error variable == true
 * @param  values          The keys the values to get are stored with
 * @param  settings             Object containing the settings for getting the variables
 * @param  settings.validator   The validator is an object containing functions which take a value and tests whether the value matches to required format returning true for a valid parameter and false for invalid. Or it can return a custom error message as a string. It is also possible to return mutliple error messages as an array of strings. The keys of the validator should be identical to the queryparam keys.
 * @param  settings.prefix      The prefix to use for the variables which will be used to set the potential error messages
 * @return                      A boolean indicating whether the query param were valid or not
 */
var validateValues = exports.validateValues = function validateValues(values) {
	var _ref = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {
		validator: {},
		prefix: ''
	},
	    _ref$validator = _ref.validator,
	    validator = _ref$validator === undefined ? {} : _ref$validator,
	    _ref$prefix = _ref.prefix,
	    prefix = _ref$prefix === undefined ? '' : _ref$prefix;

	var error = {
		error: false,
		payload: {
			errors: []
		}
	};

	Object.keys(values).forEach(function (key) {
		if (validator[key]) {
			var validatorResponse = validator[key](values[key]);
			var invalidResponse = validatorResponse === false || typeof validatorResponse === 'string' && validatorResponse !== '' || Array.isArray(validatorResponse) && !!validatorResponse.length;

			if (invalidResponse) {
				var errorMessage = typeof validatorResponse === 'boolean' ? '' : validatorResponse;

				error.payload.errors = !Array.isArray(errorMessage) ? [].concat(_toConsumableArray(error.payload.errors), [createErrorObject(key, values[key], errorMessage)]) : [].concat(_toConsumableArray(error.payload.errors), _toConsumableArray(errorMessage.map(function (singleErrorMessage) {
					return createErrorObject(key, values[key], singleErrorMessage);
				})));
			}
		}
	});

	if (error.payload.errors.length) {
		error.error = true;
		error.payload = {
			title: 'Invalid parameter',
			message: 'One or more parameters are invalid',
			statusCode: 400,
			errors: error.payload.errors
		};
	}

	(0, _setVariables2.default)({
		error: error.error,
		errorpayload: JSON.stringify(error.payload)
	}, {
		prefix: prefix
	});

	return !error.error;
};

/**
 * This will create the default error message
 * @param  key      The key of the query parameter
 * @param  value    The value of the query parameter
 * @param  message  The custom message to use
 * @return          A default error object
 */
var createErrorObject = function createErrorObject(key, value, message) {
	return {
		title: 'Invalid ' + key + ' query parameter',
		message: message === '' ? 'Invalid ' + key + ' parameter. You passed "' + value + '".' : message,
		source: key
	};
};

exports.default = validateValues;
});

unwrapExports(validateValues_1);
var validateValues_2 = validateValues_1.validateValues;

var validateQueryParams_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.validateQueryParams = undefined;



var _validateValues2 = _interopRequireDefault(validateValues_1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

var validateQueryParams = exports.validateQueryParams = _validateValues2.default;
exports.default = validateQueryParams;
});

unwrapExports(validateQueryParams_1);
var validateQueryParams_2 = validateQueryParams_1.validateQueryParams;

var setQueryParam_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});


/**
 * This will set a query parameter to the provided value
 * @param  key   The key of the queryparam to set
 * @param  value The value to set the queryparam to
 */
var setQueryParam = exports.setQueryParam = function setQueryParam(key, value) {
  return context.setVariable("request.queryparam." + key, value);
};

exports.default = setQueryParam;
});

unwrapExports(setQueryParam_1);
var setQueryParam_2 = setQueryParam_1.setQueryParam;

var setQueryParams_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.setQueryParams = undefined;



var _setQueryParam2 = _interopRequireDefault(setQueryParam_1);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

/**
 * This will convert an object with key value pairs to query parameters
 * @param  queryParams   An object containing key value pairs to be used as query parameters
 */
var setQueryParams = exports.setQueryParams = function setQueryParams(queryParams) {
  return Object.keys(queryParams).forEach(function (key) {
    return (0, _setQueryParam2.default)(key, queryParams[key]);
  });
};
exports.default = setQueryParams;
});

unwrapExports(setQueryParams_1);
var setQueryParams_2 = setQueryParams_1.setQueryParams;

var validateBoolean_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
  value: true
});


/**
 * This will do a simple check if the passed string is a stringified boolean or not
 * @param settings          An object containing the options for validation
 * @param settings.name     The name of the variable to check
 * @param settings.value    The value of the variable to check
 * @param settings.required Whether it's required (allow undefined values or not)
 * @return                  A default error message or an empty string
 */
var validateBoolean = exports.validateBoolean = function validateBoolean(_ref) {
  var name = _ref.name,
      value = _ref.value,
      required = _ref.required;
  return required && value === undefined || value !== undefined && value !== 'true' && value !== 'false' ? 'Valid ' + name + ' parameters are "true" and "false". You passed "' + value + '".' : '';
};

exports.default = validateBoolean;
});

unwrapExports(validateBoolean_1);
var validateBoolean_2 = validateBoolean_1.validateBoolean;

var validateEnum_1 = createCommonjsModule(function (module, exports) {
Object.defineProperty(exports, "__esModule", {
	value: true
});


/**
 * This will do a simple check if the passed value is one of the valid values
 * @param settings             An object containing the options for validation
 * @param settings.name        The name of the variable to check
 * @param settings.value       The value of the variable to check
 * @param settings.validValues The possible values
 * @param settings.required    Whether it's required (allow undefined values or not)
 * @return                     A default error message or an empty string
 */
var validateEnum = exports.validateEnum = function validateEnum(_ref) {
	var name = _ref.name,
	    value = _ref.value,
	    required = _ref.required,
	    validValues = _ref.validValues;
	return required && value === undefined || value !== undefined && !(validValues.indexOf(value) !== -1) ? 'Valid ' + name + ' parameters are ' + validValues.join(', ') + '. You passed ' + value + '.' : '';
};

exports.default = validateEnum;
});

unwrapExports(validateEnum_1);
var validateEnum_2 = validateEnum_1.validateEnum;

/**
 * This is a list of the possible parameters which can be used for this API
 */
var optionalQueryParams = ['transportMode', 'instructions', 'polyline', 'mergePolylines'];

/**
 * This is a set of default values to use when nothing is specified
 */

var toConsumableArray = function (arr) {
  if (Array.isArray(arr)) {
    for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) arr2[i] = arr[i];

    return arr2;
  } else {
    return Array.from(arr);
  }
};

var tomtomPostFlow = function tomtomPostFlow() {
	var proxyResponse = getProxyResponse();

	variables = getVariables(optionalQueryParams, {
		prefix: 'anwbonline.',
		parser: parser
	});

	var ANWBResponse = convertTomTomResponse(proxyResponse);

	setResponse(ANWBResponse);
};

/**
 * The stored set of variables which are received by url parameters
 */
var variables = {};

/**
 * This will parse all passed in variables to the correct format
 */
var parser = {
	instructions: function instructions(_instructions) {
		return _instructions === 'true';
	},
	polyline: function polyline(_polyline) {
		return _polyline === 'true';
	},
	mergePolylines: function mergePolylines(_mergePolylines) {
		return _mergePolylines === 'true';
	}
};

/**
 * This function will start converting the response from TomTom to the ANWB format
 * @param  tomtomResponse 	The entire raw response from TomTom
 * @return 					The entire formatted ANWB response
 */
var convertTomTomResponse = function convertTomTomResponse(tomtomResponse) {
	if (tomtomResponse.routes && tomtomResponse.routes.length) {
		return {
			success: true,
			routes: tomtomResponse.routes.map(function (tomtomRoute) {
				lastInstruction = 0;
				return getANWBRoute(tomtomRoute);
			})
		};
	} else {
		response.status.code = 500;
		logMessage();

		return {
			title: 'TomTom feed is not available',
			message: tomtomResponse.error ? tomtomResponse.error.description : undefined,
			statusCode: 500
		};
	}
};

/**
 * This will reset the response to the ANWB formatted object of the route
 * @param ANWBResponse An object containing the route in our formatting
 */


/**
 * This array is used to store all roadnumbers found in the route
 */
var routeRoadNumbers = [];

/**
 * This function will convert a TomTom route to an ANWB route
 * @param  tomtomRoute 	The route representation TomTom provides us
 * @return 				The route representation ANWB prefers
 */
var getANWBRoute = function getANWBRoute(tomtomRoute) {
	return {
		id: getGuid(),
		legs: tomtomRoute.legs.map(function (tomtomLeg) {
			return getANWBLeg(tomtomLeg, tomtomRoute);
        }),
        polyline: variables.mergePolylines ? mergePolylines(tomtomRoute) : undefined,
		summary: {
			distance: tomtomRoute.summary.lengthInMeters,
			duration: tomtomRoute.summary.travelTimeInSeconds,
			durationNoTraffic: tomtomRoute.summary.noTrafficTravelTimeInSeconds ? tomtomRoute.summary.noTrafficTravelTimeInSeconds : undefined,
			delay: tomtomRoute.summary.trafficDelayInSeconds,
			departure: toISOString(tomtomRoute.summary.departureTime),
			arrival: toISOString(tomtomRoute.summary.arrivalTime),
			roadNumbers: routeRoadNumbers.length ? getANWBRouteRoadNumbers(routeRoadNumbers) : undefined
		}
	};
};

/**
 * This function will merge the polylines for all routes to a single encoded polyline string
 * @param  tomtomRoute The route representation TomTom provides us
 * @return             A google encoded polyline representing the entire route
 */
var mergePolylines = function mergePolylines(tomtomRoute) {
	var polyline = tomtomRoute.legs.reduce(function (polyline, tomtomLeg) {
		return [].concat(toConsumableArray(polyline), toConsumableArray(tomtomLeg.points.map(function (tomtomPoint) {
			return getANWBPoint(tomtomPoint);
		})));
	}, []);

	return encodePolyline(polyline);
};

/**
 * This will filter out all duplicates from all roadNumbers collected for this route
 * @param  roadNumbers	An array with possible duplicate roadnumbers
 * @return 				An array with unique roadNumbers
 */
var getANWBRouteRoadNumbers = function getANWBRouteRoadNumbers(roadNumbers) {
	var filteredRoadNumbers = roadNumbers.filter(function (roadNumber, index) {
		return roadNumbers.indexOf(roadNumber) === index;
	});

	routeRoadNumbers = [];

	return filteredRoadNumbers;
};

/**
 * This number is used to split the route instructions into multiple parts so we can pick up where we stopped
 */
var lastInstruction = 0;

/**
 * This array is used to store the roadnumbers for each leg
 */
var legRoadNumbers = [];

/**
 * This function will fetch a leg for a route and will convert it to ANWB format
 * @param  tomtomLeg   	The leg representation TomTom provides us
 * @param  tomtomRoute 	The route representation TomTom provides us
 * @return 				The leg representation ANWB prefers
 */
var getANWBLeg = function getANWBLeg(tomtomLeg, tomtomRoute) {
	var ANWBLeg = {
		summary: {
			distance: tomtomLeg.summary.lengthInMeters,
			duration: tomtomLeg.summary.travelTimeInSeconds,
			durationNoTraffic: tomtomLeg.summary.noTrafficTravelTimeInSeconds ? tomtomLeg.summary.noTrafficTravelTimeInSeconds : undefined,
			delay: tomtomLeg.summary.trafficDelayInSeconds,
			departure: toISOString(tomtomLeg.summary.departureTime),
			arrival: toISOString(tomtomLeg.summary.arrivalTime)
        },
        points: tomtomLeg.points,
		polyline: variables.polyline ? getLegPolyline(tomtomLeg.points) : undefined,
		instructions: variables.instructions ? [] : undefined,
		transportMode: 'car'
	};
	var ANWBInstruction = {};

	for (var i = lastInstruction; i < tomtomRoute.guidance.instructions.length; i++) {
		if (tomtomRoute.guidance.instructions[i].maneuver !== 'FOLLOW') {
			ANWBInstruction = getANWBInstruction(tomtomRoute.guidance.instructions[i]);

			if (variables.instructions && ANWBLeg.instructions) ANWBLeg.instructions.push(ANWBInstruction);

			// When we encounter a LOCATION_WAYPOINT it means we have hit an end to this leg and we need to stop the loop
			if (tomtomRoute.guidance.instructions[i].instructionType === 'LOCATION_WAYPOINT') {
				lastInstruction = i + 1;
				i = tomtomRoute.guidance.instructions.length;
			}
		}
	}

	if (legRoadNumbers.length) ANWBLeg.summary.roadNumbers = addRouteRoadNumbers(legRoadNumbers);

	return ANWBLeg;
};

/**
 * This will all roadNumbers from the current leg to the collection of route roadNumbers
 * @param  roadNumbers The roadNumbers relevant for the current leg
 * @return             The roadNumbers relevant for the current leg deduped
 */
var addRouteRoadNumbers = function addRouteRoadNumbers(roadNumbers) {
	var filteredRoadNumbers = roadNumbers.filter(function (roadNumber, index) {
		return roadNumbers.indexOf(roadNumber) === index;
	});

	routeRoadNumbers = [].concat(toConsumableArray(routeRoadNumbers), toConsumableArray(filteredRoadNumbers));

	legRoadNumbers = [];

	return filteredRoadNumbers;
};

/**
 * This will convert an array of TomTomPoints to a google encoded polyline
 * @param  tomtomPoints	An array of tomtomPoints
 * @return              A Google encoded polyline
 */
var getLegPolyline = function getLegPolyline(tomtomPoints) {
	return encodePolyline(tomtomPoints.map(function (tomtomPoint) {
		return getANWBPoint(tomtomPoint);
	}));
};

/**
 * This will convert a TomTom point to a simple array of lat lon
 * @param  tomtomPoint 	An object containing latitude and longitude
 * @return 				An array representing a location on a map
 */
var getANWBPoint = function getANWBPoint(tomtomPoint) {
	return [tomtomPoint.latitude, tomtomPoint.longitude];
};

/**
 * This will convert a TomTom point to a simple lat lon object
 * @param  lat 	The latitude of the provided location
 * @param  lon 	The longitude of the provided location
 * @return 		An object containing the exact geographical location
 */
var createLocation = function createLocation(lat, lon) {
	return {
		lat: lat,
		lon: lon
	};
};

/**
 * This will extract an instruction from the feed using the formatting ANWB prefers
 * @param  tomtomInstruction 	The instruction representation TomTom provides us
 * @return 						The instruction representation ANWB prefers
 */
var getANWBInstruction = function getANWBInstruction(tomtomInstruction) {
	return {
		point: createLocation(tomtomInstruction.point.latitude, tomtomInstruction.point.longitude),
		distance: tomtomInstruction.routeOffsetInMeters,
		duration: tomtomInstruction.travelTimeInSeconds,
		instructionType: tomtomInstruction.instructionType,
		countryCode: tomtomInstruction.countryCode,
		drivingSide: tomtomInstruction.drivingSide,
		message: tomtomInstruction.message,
		direction: tomtomInstruction.signpostText,
		maneuver: tomtomInstruction.maneuver,
		exitNumber: tomtomInstruction.exitNumber ? tomtomInstruction.exitNumber : undefined,
		street: tomtomInstruction.street ? tomtomInstruction.street : undefined,
		roadNumbers: tomtomInstruction.roadNumbers ? addLegRoadNumbers(tomtomInstruction.roadNumbers) : undefined
	};
};

/**
 * This will all roadNumbers from the current instruction to the collection of leg roadNumbers
 * @param  roadNumbers The roadNumbers relevant for the current instruction
 * @return             The roadNumbers relevant for the current instruction
 */
var addLegRoadNumbers = function addLegRoadNumbers(roadNumbers) {
	legRoadNumbers = [].concat(toConsumableArray(legRoadNumbers), toConsumableArray(roadNumbers));

	return roadNumbers;
};

/**
 * This will convert a TomTom formatted dateString to universal ISO format
 * @param  dateString A datestring provided by TomTom
 * @return            An ISO formatted datestring
 */
var toISOString = function toISOString(dateString) {
	return new Date(dateString).toISOString();
};

/**
 * This function will generate a GUID which can be used to store the route in some kind of database
 * @return A unique GUID
 */
var getGuid = function getGuid() {
	return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = Math.random() * 16 | 0;
		var v = c == 'x' ? r : r & 0x3 | 0x8;
		return v.toString(16);
	});
};

/**
 * This is an encode function which takes in a polyline and generates the Google Encoded version
 * @param  locationCoord A number representing the location
 * @param  factor        The factor the be used for precision
 * @return               A piece of a google encoded polyline
 */
var encode = function encode(locationCoord, factor) {
	var coordinate = Math.round(locationCoord * factor);
	coordinate <<= 1;
	if (coordinate < 0) {
		coordinate = ~coordinate;
	}
	var output = '';
	while (coordinate >= 0x20) {
		output += String.fromCharCode((0x20 | coordinate & 0x1f) + 63);
		coordinate >>= 5;
	}
	output += String.fromCharCode(coordinate + 63);
	return output;
};

/**
 * Encodes the given [latitude, longitude] coordinates array.
 * @param coordinates The raw polyline
 * @param precision   The precision of the output
 * @return            A Google Encoded Polyline representation of a regular polyline
 */
var encodePolyline = function encodePolyline(coordinates, precision) {
	if (!coordinates.length) {
		return '';
	}

	var factor = Math.pow(10, precision || 5);
	var output = encode(coordinates[0][0], factor) + encode(coordinates[0][1], factor);

	for (var i = 1; i < coordinates.length; i++) {
		var a = coordinates[i];
		var b = coordinates[i - 1];
		output += encode(a[0] - b[0], factor);
		output += encode(a[1] - b[1], factor);
	}

	return output;
};

tomtomPostFlow();
