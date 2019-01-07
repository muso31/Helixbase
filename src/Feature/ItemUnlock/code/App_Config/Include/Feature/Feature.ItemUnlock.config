<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <accessRights>
      <rights>
        <add patch:before="add[@name='*']" name="item:checkin" comment="The right to unlock an item." title="Item Unlock" modifiesData="true"/>
      </rights>
    </accessRights>
    <commands>
      <command name="item:checkin">
        <patch:attribute name="type">Helixbase.Feature.ItemUnlock.Commands.Item.CheckIn,Helixbase.Feature.ItemUnlock</patch:attribute>
      </command>
      <command name="contenteditor:edit">
        <patch:attribute name="type">Helixbase.Feature.ItemUnlock.Commands.ContentEditor.Edit,Helixbase.Feature.ItemUnlock</patch:attribute>
      </command>
    </commands>
    <pipelines>
      <getContentEditorWarnings>
        <processor type="Sitecore.Pipelines.GetContentEditorWarnings.IsLocked, Sitecore.Kernel">
          <patch:attribute name="type">Helixbase.Feature.ItemUnlock.Pipelines.GetContentEditorWarnings.IsLocked,Helixbase.Feature.ItemUnlock</patch:attribute>
        </processor>
      </getContentEditorWarnings>
    </pipelines>
    <events>
      <event name="user:deleted">
        <handler type="Helixbase.Feature.ItemUnlock.Events.RemovedUserEventHandler, Helixbase.Feature.ItemUnlock" method="OnUserDeletedUnlockFiles" />
      </event>
    </events>
  </sitecore>
</configuration>