import ko from 'knockout';
export function asyncComputed(evaluator, owner) {
    var result = ko.observable();

    ko.computed(function () {
        // Get the $.Deferred value, and then set up a callback so that when it's done,
        // the output is transferred onto our "result" observable
        evaluator.call(owner).then(result);
    });

    return result;
}