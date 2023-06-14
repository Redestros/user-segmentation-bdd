import { Client } from "@notionhq/client";

const notion = new Client({
  auth: process.env.NOTION_SECRET,
});

export class NotionDoc {
  /**
   *
   * @param {String} id the document id
   */
  constructor(id) {
    this.id = id;
  }

  async get() {
    return notion.databases.retrieve({ database_id: this.id });
  }

  async clear() {
    const response = await notion.databases.query({ database_id: this.id });
    if (response.results.length) {
      response.results.forEach((page) => {
        notion.pages.update({ page_id: page.id, archived: true });
      });
    }
  }

  /**
   *
   * @param {String} name
   * @param {Array} scenarios
   * @param {Array} categories
   * @param {Array} tags
   * @returns
   */
  async createItem(name, scenarios, categories, tags) {
    let properties = {
      scenario: {
        type: "rich_text",
        rich_text: [
          {
            type: "text",
            text: {
              content: scenarios.join("\n"),
            },
            plain_text: scenarios.join("\n"),
          },
        ],
      },
      feature: {
        id: "title",
        type: "title",
        title: [
          {
            type: "text",
            text: {
              content: name,
            },
            plain_text: name,
          },
        ],
      },
    };
    categories.forEach((category) => {
      properties[category] = {
        type: "multi_select",
        multi_select: [],
      };
    });
    tags.forEach((tag) => {
      properties[tag.category].multi_select.push({ name: tag.value });
    });
    return notion.pages.create({
      parent: {
        type: "database_id",
        database_id: this.id,
      },
      properties: properties,
    });
  }

  /**
   * 
   * @param {String} pageId 
   * @param {String} code 
   * @returns 
   */
  async addGherkinBlock(pageId, code) {
    return notion.blocks.children.append({
      block_id: pageId,
      children: [
        {
          type: "code",
          code: {
            caption: [],
            rich_text: [
              {
                type: "text",
                text: {
                  content: code,
                },
              },
            ],
            language: "gherkin",
          },
        },
      ],
    });
  }
}
