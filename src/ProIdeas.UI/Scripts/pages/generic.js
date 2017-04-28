import BasePage from '../basePage';

class GenericPage extends BasePage {
    constructor() {
        super(...arguments);
    }

    onReady() {
        return Promise.resolve(true);
    }
}
const page = new GenericPage();
page.init();