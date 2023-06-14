import { convertFeatureFileToJSON } from "gherkin-parse";

export class FeatureObjectHandler {
  /**
   * @param {File} file 
   */
  constructor(file) {
    this.object = convertFeatureFileToJSON(file).feature;
  }
  /**
   *
   * @returns {string} name
   */
  getName() {
    return this.object.name;
  }
   /**
   *
   * @returns {Array} scenarios
   */
  getScenarios() {
    let scenarios = [];
    this.object.children.forEach((child) => {
      child.type === "Scenario" && scenarios.push(child);
    });
    return scenarios;
  }

  /**
   *
   * @returns {Array} scenarios names
   */
  getScenariosNames() {
    return this.getScenarios().map((scenario) => scenario.name);
  }
  /**
   *
   * @returns {Array} tags
   */
  getTags() {
    let tags = [];
    this.object.tags.forEach((tag) => {
      tags.push(tag);
    });
    this.object.children.forEach((child) => {
      if (child?.tags) {
        child.tags.forEach((tag) => {
          tags.push(tag);
        });
      }
    });
    return tags;
  }

  /**
   *
   * @returns {Array} tags as objects {category: "xxx", value: "yyy"}
   */
  getTagsAsObject() {
    const tags = this.getTags();
    const tagsAsObjects = tags.map((tag) => {
      let regExp = new RegExp("(?<=@).*(?=_.*)");
      const category = regExp.exec(tag.name)[0];
      regExp = new RegExp("(?<=@.*_).*");
      const value = regExp.exec(tag.name)[0];
      return { category: category, value: value };
    });
    return [...new Set(tagsAsObjects)];
  }

  /**
   *
   * @returns {Array} categories of tags
   */
  getTagsCategories() {
    const tags = this.getTagsAsObject();
    return [
      ...new Set(
        tags.map((tag) => {
          return tag.category;
        })
      ),
    ];
  }
}
