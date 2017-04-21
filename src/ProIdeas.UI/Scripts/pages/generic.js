import BasePage from '../basePage';

class GenericPage extends BasePage {
    constructor() {
        super(...arguments);
    }

    onReady() {
        
    }
}
const page = new GenericPage();
page.init();