#include <iostream>
#include <string>
#include "pugixml.hpp"

class XMLParser {
private:
    pugi::xml_document m_doc;
    pugi::xml_node m_node;
public:
    void loadFile(const char* filePath);

    void getNode(const char* nodeName);
    std::string getAttribute(const char* attributeName);
    std::string getValue(const char* nodeName);
};

void XMLParser::loadFile(const char* filePath) {
    pugi::xml_parse_result result = m_doc.load_file(filePath);
    if (!result) {
        std::cerr << "XML [" << filePath << "] parsed with errors\n";
    }
}

void XMLParser::getNode(const char* nodeName) {
    m_node = m_doc.child(nodeName);
}

std::string XMLParser::getAttribute(const char* attributeName) {
    pugi::xml_node node = m_doc.child(attributeName);
    if (node) {
        return node.attribute(attributeName).value();
    }
    return "";
}