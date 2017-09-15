import { IntgDashPage } from './app.po';

describe('intg-dash App', () => {
  let page: IntgDashPage;

  beforeEach(() => {
    page = new IntgDashPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
