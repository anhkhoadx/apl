import { SampleWebStoreTemplatePage } from './app.po';

describe('SampleWebStore App', function() {
  let page: SampleWebStoreTemplatePage;

  beforeEach(() => {
    page = new SampleWebStoreTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
